﻿using Microsoft.AspNetCore.Mvc;
using ScarletScreen.Model;
using ScarletScreen.Services;

public class HomeController : Controller
{
    private readonly MongoDBService _databaseService;
    private readonly TMDbService _tmdbService;

    public HomeController(MongoDBService databaseService, TMDbService tmdbService)
    {
        _databaseService = databaseService;
        _tmdbService = tmdbService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeModel();

        // Get trending movie IDs
        viewModel.TrendingMovieIds = await _tmdbService.GetTrendingMovieIds();

        return View(viewModel);



    }


    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        List<TmdbResult> tmdbResults = await _tmdbService.SearchTmdb(query);

        // Create an instance of SearchViewModel
        SearchViewModel searchViewModel = new SearchViewModel
        {
            TmdbResults = tmdbResults
        };

        return View(searchViewModel);
    }

}