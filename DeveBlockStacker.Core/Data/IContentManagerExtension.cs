using Microsoft.Xna.Framework.Content;
using System.IO;

namespace DeveBlockStacker.Core.Data
{
    public interface IContentManagerExtension
    {
        Stream OpenStream(ContentManager content, string assetName);
    }
}
