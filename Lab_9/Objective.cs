using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_9
{
    internal class Objective
    {
        public string Code { get; }
        public string Description { get; }
        public int RequiredCount { get; }

        public Objective(string code, string description, int requiredCount)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be null or whitespace.", nameof(code));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or whitespace.", nameof(description));

            if (requiredCount < 1)
                throw new ArgumentException("RequiredCount must be at least 1.", nameof(requiredCount));

            Code = code;
            Description = description;
            RequiredCount = requiredCount;
        }

        public override string ToString() =>
            $"{Code}: {Description} (x{RequiredCount})";
    }
}
