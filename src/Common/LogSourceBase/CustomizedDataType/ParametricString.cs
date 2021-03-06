﻿using LogFlow.DataModel.Algorithm;

namespace LogFlow.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParametricString
    {
        private readonly string storedString;
        private const string LeftSign = "{{{";
        private const string RightSign = "}}}";

        public ParametricString(string template, params string[] parameters)
        {
            this.storedString = LocalStringPool.Intern(string.Format(template, parameters.Select(p => (object)$"{LeftSign}{p}{RightSign}").ToArray()));
        }

        public override string ToString()
        {
            return string.Concat(this.GetTokens().Select(t => t.Key));
        }

        public string ToHtml()
        {
            return string.Concat(this.GetTokens().Select(t => t.Value ? $"<b>{t.Key}</b>" : t.Key));
        }

        public IEnumerable<KeyValuePair<string, bool>> GetTokens()
        {
            //todo : improve the perf for drawing.

            int p = 0;
            bool isParameter = false;

            while (p < this.storedString.Length)
            {
                int l = this.storedString.IndexOf(LeftSign, p, StringComparison.Ordinal);
                if (l == -1) l = this.storedString.Length;
                int r = this.storedString.IndexOf(RightSign, p, StringComparison.Ordinal);
                if (r == -1) r = this.storedString.Length;

                int next = Math.Min(l, r);
                if (next > p)
                {
                    yield return new KeyValuePair<string, bool>(this.storedString.Substring(p, next - p), isParameter);
                }

                isParameter = l < r;
                p = next + LeftSign.Length;
            }
        }
    }
}
