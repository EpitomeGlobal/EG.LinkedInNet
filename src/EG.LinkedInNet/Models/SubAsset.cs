namespace EG.LinkedInNet.Models;

using System.Text.Json.Serialization;

public class SubAsset
{
    /// <summary>
    /// The learning asset that is a sub-asset of another learning asset.
    /// </summary>
    public AssetDetails Asset { get; init; }
}
