using MedflixAPI.Services.Torrent.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedflixAPI.Services.Torrent
{
    internal interface ITorrentSearcher
    {
        Task<IEnumerable<MediaTorrent>> GetTorrentLinksAsync(TorrentRequest torrentRequest);
    }
}
