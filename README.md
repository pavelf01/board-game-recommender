# board-game-recommender

For frontend boilerplate used: https://github.com/catalin-luntraru/redux-minimal

How to start FE:
 - npm install
 - npm run start 
 
Entry point of backend application is API project. 

For working API "from scratch" recreate mysql database from this dumped sql file:
https://is.muni.cz/de/410036/db_final.sql
and set proper connection string in API\Web.config


# Project structure

Frontend:

Backend:
 - 4 layer application structure
 - API project operating requests
 - DAL containing entity framework entities mapped to database tables
 - BL entity repositories, services with business logic
 - BL.Services.RecommenderEngine namespace contains all reccommendation services including all recommender algorithms


# Team Roles

Pavej Fojtík - Frontend, API
Aleš Kopecký - Data import, Databse structure, Model, API
Simona Kruppová - API, Content-based recommender, Random recommender