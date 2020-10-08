//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Linq;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// a static block array 'builder'.
    /// </summary>
    public static class ArrayBlock
    {
        /// <summary>
        /// Combines the specified arrays.
        /// we don't know how big the arrays are so it's would be a good idea to consider some level of efficiency.
        /// </summary>
        /// <param name="arrays">the arrays.</param>
        /// <returns>the newly combined byte array.</returns>
        public static byte[] Combine(params byte[][] arrays)
        {
            var usableArrays = arrays.Where(x => It.Has(x)).AsSafeList();
            var ret = new byte[usableArrays.Sum(x => x.Length)];

            if (ret.Length > 0)
            {
                int offset = 0;
                usableArrays.ForEach(x =>
                {
                    Buffer.BlockCopy(x, 0, ret, offset, x.Length);
                    offset += x.Length;
                });
            }

            return ret;
        }
    }
}
