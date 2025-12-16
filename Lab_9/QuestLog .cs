using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_9
{
    internal class QuestLog : IEnumerable<Quest>
    {
        private readonly List<Quest> _quests = new();
        private readonly Dictionary<string, Quest> _byId = new();

        public int Count => _quests.Count;

        public Quest this[int index]
        {
            get
            {
                if (index < 0 || index >= _quests.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _quests[index];
            }
        }

        public Quest this[string id]
        {
            get
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));
                if (!_byId.TryGetValue(id, out var quest))
                    throw new KeyNotFoundException($"Quest with Id '{id}' not found.");
                return quest;
            }
        }

        public void Add(Quest quest)
        {
            if (quest == null)
                throw new ArgumentNullException(nameof(quest));

            if (_byId.ContainsKey(quest.Id))
                throw new ArgumentException($"Quest with Id '{quest.Id}' already exists.", nameof(quest));

            _quests.Add(quest);
            _byId[quest.Id] = quest;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _quests.Count)
                return false;

            var quest = _quests[index];
            _quests.RemoveAt(index);
            _byId.Remove(quest.Id);
            return true;
        }

        public bool RemoveById(string id)
        {
            if (id == null || !_byId.TryGetValue(id, out var quest))
                return false;

            _quests.Remove(quest);
            _byId.Remove(id);
            return true;
        }

        public IEnumerable<Quest> EnumerateByDifficulty(Difficulty minDifficulty)
        {
            foreach (var quest in _quests)
            {
                if ((int)quest.Difficulty >= (int)minDifficulty)
                    yield return quest;
            }
        }

        public IEnumerator<Quest> GetEnumerator() => _quests.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
