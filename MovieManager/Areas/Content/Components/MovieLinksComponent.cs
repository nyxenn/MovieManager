using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Internal.DataAccess;
using MovieManager.Areas.Content.Models;
using MovieManager.Areas.Content.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.Components
{
    [ViewComponent(Name = "MovieLinks")]
    public class MovieLinksComponent : ViewComponent
    {

        public MovieLinksComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(string links = "", string type = "")
        {
            MovieLinksViewModel linksVM = new MovieLinksViewModel();
            List<string> linksList = new List<string>();

            if (links != "N/A")
            {
                string[] linksSplit = links.Split(',');
                foreach (var link in linksSplit)
                {
                    linksList.Add(link.Trim());
                }
            }
            else
            {
                linksList.Add("N/A");
            }

            linksVM.Links = linksList;
            linksVM.Type = type;

            return View(linksVM);
        }
    }
}
