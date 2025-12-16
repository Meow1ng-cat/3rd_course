using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_9
{
    internal class Quest
    {
        public string Id { get; }
        public string Title { get; }
        public Difficulty Difficulty { get; }
        public IReadOnlyList<Objective> Objectives { get; }

        private readonly List<Objective> _objectives;

        public Quest(string id, string title, Difficulty difficulty, IEnumerable<Objective> objectives)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id cannot be null or whitespace.", nameof(id));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or whitespace.", nameof(title));

            Id = id;
            Title = title;
            Difficulty = difficulty;
            _objectives = objectives?.ToList() ?? new List<Objective>();
            Objectives = new ReadOnlyCollection<Objective>(_objectives);
        }

        // Для внутреннего добавления целей (если нужно после создания)
        internal void AddObjective(Objective objective)
        {
            _objectives.Add(objective ?? throw new ArgumentNullException(nameof(objective)));
        }

        public override string ToString() =>
            $"{Id}: {Title} [{Difficulty}] ({Objectives.Count} objectives)";
    }
}
