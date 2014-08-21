using System;
using System.Collections.Generic;
using System.Linq;

namespace Tanpohp.Utils.Environment
{
    public class EnvironmentVariableProvider : IEnvironmentVariableProvider
    {
        private readonly uint _millisBetweenRefresh;
        private readonly static object LockKey = new object();

        private readonly Dictionary<EnvironmentVariableTarget, Dictionary<string, VariableBag>> _variables;

        public EnvironmentVariableProvider(uint millisBetweenRefresh = (uint)5000, bool loadAllVariables = false)
        {
            _millisBetweenRefresh = millisBetweenRefresh;
            _variables = new Dictionary<EnvironmentVariableTarget, Dictionary<string, VariableBag>>
                             {
                                 {EnvironmentVariableTarget.Machine, new Dictionary<string, VariableBag>(16)},
                                 {EnvironmentVariableTarget.Process, new Dictionary<string, VariableBag>(16)},
                                 {EnvironmentVariableTarget.User, new Dictionary<string, VariableBag>(16)}
                             };
            
            if (!loadAllVariables) return;
            
            LoadAllVariables(EnvironmentVariableTarget.Machine);
            LoadAllVariables(EnvironmentVariableTarget.Process);
            LoadAllVariables(EnvironmentVariableTarget.User);
        }

        private void LoadAllVariables(EnvironmentVariableTarget target)
        {
            var variables = System.Environment.GetEnvironmentVariables(target);
            var targetVariables = _variables[target];
            foreach (var variableName in variables.Keys.Cast<string>())
            {
                var value = variables[variableName] as string;

                targetVariables.Add(variableName, new VariableBag(variableName, value, target));
            }
        }

        public string Get(string variableName, EnvironmentVariableTarget target)
        {
            lock (LockKey)
            {
                return _variables[target].ContainsKey(variableName)
                           ? GetExisting(target, variableName)
                           : GetNew(target, variableName);
            }
        }

        private string GetNew(EnvironmentVariableTarget target, string variableName)
        {
            var variableBag = new VariableBag(variableName, null, target);
            UpdateVariable(variableBag);
            _variables[target].Add(variableName, variableBag);

            return variableBag.Content;
        }

        private string GetExisting(EnvironmentVariableTarget target, string variableName)
        {
            var existingVariable = _variables[target][variableName];
            if (!IsValid(existingVariable)) UpdateVariable(existingVariable);

            return existingVariable.Content;
        }

        private static void UpdateVariable(VariableBag existingVariable)
        {
            existingVariable.Content = System.Environment.GetEnvironmentVariable(existingVariable.Name, existingVariable.Target);
        }

        private bool IsValid(VariableBag existingVariable)
        {
            return (DateTime.Now - existingVariable.LastUpdate).TotalMilliseconds < _millisBetweenRefresh;
        }

        public bool Exists(string variableName, EnvironmentVariableTarget target)
        {
            lock (LockKey)
            {
                if (_variables[target].ContainsKey(variableName) && _variables[target][variableName].Content != null) return true;

                var variable = System.Environment.GetEnvironmentVariable(variableName, target);

                return variable != null;
            }
        }

        public void Refresh()
        {
            lock (LockKey)
            {
                foreach (var variableBag in _variables.SelectMany(variableDictionary => variableDictionary.Value))
                {
                    UpdateVariable(variableBag.Value);
                }
            }
        }

        private class VariableBag
        {
            public string Name { get; private set; }
            private string _content;

            public string Content
            {
                get { return _content; }
                set
                {
                    _content = value;
                    LastUpdate = DateTime.Now;
                }
            }

            public DateTime LastUpdate { get; private set; }
            public EnvironmentVariableTarget Target { get; private set; }

            public VariableBag(string name, string content, EnvironmentVariableTarget target)
            {
                Name = name;
                Content = content;
                Target = target;
            }
        }
    }
}