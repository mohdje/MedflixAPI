using MoviesAPI.Services;
using MoviesAPI.Services.Torrent;
using MoviesAPI.Services.Torrent.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApiSample.Samples
{
    internal class TorrentSample
    {
        TorrentSearchManager torrentSearchManager;
        public TorrentSample()
        {
            torrentSearchManager = MoviesAPIFactory.CreateTorrentSearchManager();
        }

        public async Task Test()
        {
            // await SearchVfMovieTorrents("Frankenstein", 2025);
            // await SearchVfSerieTorrents("Fleishman Is In Trouble", 1, 3);
            //await SearchVoMovieTorrents("Inception", 2010);
            await SearchVoSerieTorrents("Berlin and the Lady with an Ermine", "tt42178219", 1, 1);
        }
        private async Task SearchVfMovieTorrents(string frenchMovieName, int year)
        {
            Console.WriteLine($"Search vf torrents for {frenchMovieName}, {year}");

            var vfTorrents = await torrentSearchManager.SearchVfTorrentsMovieAsync(frenchMovieName, year);

            PrintResult(vfTorrents);
        }

        private async Task SearchVfSerieTorrents(string frenchTitleSerie, int seasonNumber, int episodeNumber)
        {
            Console.WriteLine($"Search vf torrents for {frenchTitleSerie}, season {seasonNumber}, episode {episodeNumber}");

            var vfTorrents = await torrentSearchManager.SearchVfTorrentsSerieAsync(frenchTitleSerie, seasonNumber, episodeNumber);

            PrintResult(vfTorrents);
        }

        private async Task SearchVoMovieTorrents(string originalTitle, int year)
        {
            Console.WriteLine($"Search vo torrents for {originalTitle}, {year}");

            var voTorrents = await torrentSearchManager.SearchVoTorrentsMovieAsync(originalTitle, year);

            PrintResult(voTorrents);
        }

        private async Task SearchVoSerieTorrents(string serieName, string imdbID, int seasonNumber, int episodeNumber)
        {
            var voTorrents = await torrentSearchManager.SearchVoTorrentsSerieAsync(serieName, imdbID, seasonNumber, episodeNumber);

            PrintResult(voTorrents);
        }

        private void PrintResult(IEnumerable<MediaTorrent> mediaTorrents)
        {
            if (mediaTorrents == null || !mediaTorrents.Any())
                Console.WriteLine("No torrent found");
            else
            {
                foreach (var torrent in mediaTorrents)
                {
                    Console.WriteLine($"Quality: {torrent.Quality}, Url: {torrent.DownloadUrl}");
                }
            }
        }

    }
}
