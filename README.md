# MoviesAPI
Comcast Freewheel Movies API Assessement



Steps:
1) Create New Project (VS2019) -> ASP.Net Core Web Application -> Web API Template
2) Install Microsoft.EntityFrameworkCore (DbContext)
3) Install Microsoft.EntityFrameworkCore.SqlServer (UseSQLServer)
4) Install Microsoft.EntityFrameworkCore.Tools (for DB Migrations and Seeding)
5) In Package Manager Console, Run below command. This creates Migration files under Migrations folder.
   Add-Migration InitialMigratioin (This is not required, as have added code to automatically Seed on Running the App)

For Tests:
1) Install Microsoft.NET.Test.Sdk
2) Install NUnit
3) Install Moq
4) Install NUnit3TestAdapter

APIs

API A: Query movie data based on provided filter criteria: title, year of release, genre(s)

	GET: 
	http://localhost:59603/movies-api/movies?title=T&year=1998&genre=drama&genre=action
	[
		{
			"id": 3,
			"title": "Terminator",
			"yearOfRelease": 1998,
			"runningTime": 180,
			"genres": "Action,Thriller",
			"averageRating": 4
		},
		{
			"id": 4,
			"title": "Titanic",
			"yearOfRelease": 1998,
			"runningTime": 200,
			"genres": "Romance,Drama,Action",
			"averageRating": 4.5
		}
	]

API B: Query top 5 movies based on total user rating
	GET: http://localhost:59603/movies-api/movies/top5movies
	[
		{
			"id": 7,
			"title": "Avatar",
			"yearOfRelease": 2010,
			"runningTime": 170,
			"genres": "Thriller",
			"averageRating": 5
		},
		{
			"id": 1,
			"title": "Die Hard",
			"yearOfRelease": 2000,
			"runningTime": 130,
			"genres": "Action,Thriller",
			"averageRating": 4.5
		},
		{
			"id": 5,
			"title": "Hangover",
			"yearOfRelease": 1994,
			"runningTime": 150,
			"genres": "Comedy,Drama,Action",
			"averageRating": 4
		},
		{
			"id": 3,
			"title": "Terminator",
			"yearOfRelease": 1998,
			"runningTime": 180,
			"genres": "Action,Thriller",
			"averageRating": 4
		},
		{
			"id": 4,
			"title": "Titanic",
			"yearOfRelease": 1998,
			"runningTime": 200,
			"genres": "Romance,Drama,Action",
			"averageRating": 4
		}
	]

API C: Query top 5 movies based on a certain user’s rating
	GET: http://localhost:59603/movies-api/movies/5
	[
		{
			"id": 7,
			"title": "Avatar",
			"yearOfRelease": 2010,
			"runningTime": 170,
			"genres": "Thriller",
			"averageRating": 5
		},
		{
			"id": 1,
			"title": "Die Hard",
			"yearOfRelease": 2000,
			"runningTime": 130,
			"genres": "Action,Thriller",
			"averageRating": 5
		},
		{
			"id": 5,
			"title": "Hangover",
			"yearOfRelease": 1994,
			"runningTime": 150,
			"genres": "Comedy,Drama,Action",
			"averageRating": 5
		},
		{
			"id": 3,
			"title": "Terminator",
			"yearOfRelease": 1998,
			"runningTime": 180,
			"genres": "Action,Thriller",
			"averageRating": 5
		},
		{
			"id": 6,
			"title": "Matrix",
			"yearOfRelease": 2005,
			"runningTime": 180,
			"genres": "Action",
			"averageRating": 4
		}
	]

API D: Add or update user rating for a movie
	POST: http://localhost:59603/movies-api/movies
	Headers: Content-Type: application/json
	BODY: 
		{
		"userid" : 1,
		 "movieid" : 4,
		 "rating" : 5
		}
	Response: Status 200 OK


Improvements:
 1) Code can be refactored. DB related contexts, entities can be put in a separate project
 2) We can add Validations (using Data Annotations)
 3) More Unit Testing
 4) Create Indexes for some of the Search Columns


