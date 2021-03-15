<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
***
***
***
*** To avoid retyping too much info. Do a search and replace for the following:
*** github_username, repo_name, twitter_handle, email, project_title, project_description
-->


<!-- PROJECT LOGO -->
<br />
<p align="center">
  <h3 align="center">MovieManager</h3>

  <p align="center">
    A web application used to easily track the movies and series you watch.
    <br />
    <br />
    <a href="http://moviemanager.redirectme.net:7777">View Demo</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
  </ol>
</details>

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/nyxenn/Living-Pokedex-Back.git
   ```
2. Create a database that the application can use
3. Update the connection string in appsettings.json
    ```
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=[YOUR DATABASE HERE];Initial Catalog=[DATABASE NAME];Integrated Security=True;Connect Timeout=60;Trusted_Connection=False;ApplicationIntent=ReadWrite;User ID=[DATABASE USER];Password=[PASSWORD];"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*"
    }
    ```
4. Apply migrations
    * In project 'MovieManager':
        ```azurepowershell
        Add-Migration "Add Identity Tables" -Context ApplicationDbContext
        ```
    * In project 'MMApi':
        ```azurepowershell
        Add-Migration "Add Movie Tables" -Context MovieContext
        ```
5. Run the project from Visual Studio
