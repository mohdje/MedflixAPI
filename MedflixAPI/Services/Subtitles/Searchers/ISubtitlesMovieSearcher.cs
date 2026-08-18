using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedflixAPI.Services.Subtitles.Searchers
{
    internal interface ISubtitlesMovieSearcher
    {
        Task<IEnumerable<string>> GetAvailableMovieSubtitlesUrlsAsync(string imdbCode, SubtitlesLanguage subtitlesLanguage);
        Task<string> DownloadSubtitlesFileAsync(string subtitlesSourceUrl);
        bool Match(string subtitlesSourceUrl);
    }
}
