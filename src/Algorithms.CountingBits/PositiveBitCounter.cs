// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;

    public static class PositiveBitCounter
    {
        public static IEnumerable<int> Count(int input)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(input, 0, nameof(input));

            List<int> result = [0];
            int mask = 0b_0000_0000_0000_0000_0000_0000_0000_0001;
            int i = 0;

            do
            {
                if ((mask & input) != 0)
                {
                    result.Add(i);
                }

                mask <<= 1;
                i++;
            }
            while (i < 32);

            result[0] = result.Count - 1;

            return result;
        }
    }
}
