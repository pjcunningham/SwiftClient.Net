using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftClient.Commands {
    public static class Extensions {

        public static IEnumerable<IEnumerable<TSource>> ChunkData<TSource>(this IEnumerable<TSource> source, int chunkSize) {
            for (int i = 0; i < source.Count(); i += chunkSize)
                yield return source.Skip(i).Take(chunkSize);
        } 
    }
}
