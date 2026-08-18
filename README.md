# MedflixAPI

[![NuGet](https://img.shields.io/nuget/v/MedflixAPI.svg)](https://www.nuget.org/packages/MedflixAPI/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512BD4.svg)](https://dotnet.microsoft.com/)

A comprehensive .NET class library that provides unified search capabilities for movies, series, subtitles, and torrents. MedflixAPI leverages the TMDB (The Movie Database) API for media information retrieval and integrates services for subtitle discovery and torrent sourcing across multiple languages and regions.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Usage](#usage)
  - [Movies and Series Search](#movies-and-series-information-search)
  - [Subtitles Search](#subtitles-search)
  - [Torrent Search](#torrent-search)
- [License](#license)

## Features

- **🎬 Movie & Series Search**: Comprehensive search capabilities via TMDB API with filtering by genre, platform, and popularity
- **🔤 Subtitle Search**: Multi-language subtitle discovery and download (English, French)
- **🌍 Torrent Search**: Source torrent files across multiple regions (Original and French versions)
- **📊 Detailed Information**: Retrieve comprehensive media details including cast, crew, ratings, and metadata
- **🎯 Platform Filtering**: Search by streaming platforms (Netflix, Disney+, etc.)

## Requirements

- **.NET Runtime**: .NET 10.0 or higher
- **TMDB API Key**: Required for movie and series searches (free to obtain)
  - Get your API key: [TMDB API Documentation](https://www.themoviedb.org/settings/api)

## Installation

Install the NuGet package:

```bash
dotnet add package MedflixAPI
```

Or via Package Manager:

```powershell
Install-Package MedflixAPI
```

## Quick Start

```csharp
using MedflixAPI.Services;

// Initialize searchers
var movieSearcher = MedflixAPIFactory.Instance.CreateMovieSearcher(apiKey);
var seriesSearcher = MedflixAPIFactory.Instance.CreateSeriesSearcher(apiKey);

// Search for movies
var movies = await movieSearcher.SearchMoviesAsync("Encanto");
foreach (var movie in movies)
{
    Console.WriteLine($"{movie.Title} ({movie.Year})");
}
```

## Usage

### Movies and Series Information Search

Initialize the searchers with your TMDB API key:

```csharp
var movieSearcher = MedflixAPIFactory.Instance.CreateMovieSearcher(apiKey);
var seriesSearcher = MedflixAPIFactory.Instance.CreateSeriesSearcher(apiKey);
```

**Search by Title**
```csharp
var movies = await movieSearcher.SearchMoviesAsync("Encanto");
var series = await seriesSearcher.SearchSeriesAsync("Breaking Bad");
```

**Search by Platform**
```csharp
var moviesPlatforms = await movieSearcher.GetMoviePlatformsAsync();
var moviesOfPlatform = await movieSearcher.GetMoviesByPlatformAsync(moviesPlatforms[0].Id, page: 1);

var seriesPlatforms = await seriesSearcher.GetSeriePlatformsAsync();
var seriesOfPlatform = await seriesSearcher.GetSeriesByPlatformAsync(seriesPlatforms[0].Id, page: 1);
```

**Search by Genre**
```csharp
var moviesGenres = await movieSearcher.GetMovieGenresAsync();
var moviesOfGenre = await movieSearcher.GetMoviesByGenreAsync(moviesGenres[0].Id, page: 1);

var seriesGenres = await seriesSearcher.GetSerieGenresAsync();
var seriesOfGenre = await seriesSearcher.GetSeriesByGenreAsync(seriesGenres[0].Id, page: 1);
```

**Get Popular Content by Platform**
```csharp
var popularMovies = await movieSearcher.GetPopularDisneyPlusMoviesAsync();
var popularSeries = await seriesSearcher.GetPopularNetflixSeriesAsync();
```

**Retrieve Detailed Information**
The methods above return basic information. For comprehensive details (cast, crew, ratings, images, etc.):

```csharp
var movies = await movieSearcher.SearchMoviesAsync("Encanto");
var movieDetails = await movieSearcher.GetMovieDetailsAsync(movies[0].Id);

var series = await seriesSearcher.SearchSeriesAsync("Breaking Bad");
var seriesDetails = await seriesSearcher.GetSerieDetailsAsync(series[0].Id);
```

### Subtitles Search

Search for and download subtitles in multiple languages:

```csharp
// Initialize the subtitles search manager
var downloadFolder = "/path/to/subtitles";
var subtitlesSearchManager = MedflixAPIFactory.Instance.CreateSubstitlesSearchManager(downloadFolder);

// Get movie details
var movies = await movieSearcher.SearchMoviesAsync("Encanto");
var movieDetails = await movieSearcher.GetMovieDetailsAsync(movies[0].Id);

// Retrieve available subtitle URLs
var subtitleUrls = await subtitlesSearchManager.GetAvailableMovieSubtitlesUrlsAsync(
    movieDetails.ImdbId, 
    SubtitlesLanguage.French
);

// For series
var seriesSubtitleUrls = await subtitlesSearchManager.GetAvailableSerieSubtitlesUrlsAsync(
    season: 1, 
    episode: 5, 
    seriesDetails.ImdbId, 
    SubtitlesLanguage.English
);

// Download and extract the subtitle file
var filePath = await subtitlesSearchManager.DownloadSubtitlesFileAsync(subtitleUrls.FirstOrDefault());
```

### Torrent Search

Search for torrent sources for movies and TV series:

```csharp
// Initialize the torrent search manager
var torrentSearchManager = MedflixAPIFactory.Instance.CreateTorrentSearchManager();

// Search for movie torrents (Original Version)
var torrentsVo = await torrentSearchManager.SearchVoTorrentsMovieAsync("The Wild Robot", 2024);

// Search for movie torrents (French Version)
var torrentsVf = await torrentSearchManager.SearchVfTorrentsMovieAsync("Le Robot Sauvage", 2024);

// Search for series torrents
var seriesTorrentsVf = await torrentSearchManager.SearchVfTorrentsSerieAsync("Breaking Bad", season: 1, episode: 3);
var seriesTorrentsVo = await torrentSearchManager.SearchVoTorrentsSerieAsync("Breaking Bad", season: 1, episode: 3);

// Process and display results
foreach (var torrent in seriesTorrentsVo)
{
    Console.WriteLine($"Quality: {torrent.Quality} | URL: {torrent.DownloadUrl}");
    // Note: DownloadUrl can be a magnet link or an HTTP URL to a .torrent file
}
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

## Support

For more information and examples, refer to the [MedflixAPISample](./MedflixAPISample/) project in the repository.
 
