# PokemonChallenge

### Created two Api's
1. /api/Pokemon/{name}
2. /api/Translated/{name}

### Followed clean code architecture
* Domain driven design - though smaller objective still for demonstration followed this design
* CQRS - Again there is no commands but just Query implemented
* Mediatr - To avoid coupling Api's and Query Handlers

### Run from Docker (Recommended)
1. Run `git clone https://github.com/mkiruba/PokemonChallenge.git`
2. Navigate to PokemonChallenge folder(where PokemonChallenge.sln file exists) in command line
3. Run `docker build . -f PokemonChallenge.Api\Dockerfile` (Need to run this from parent folder)
4. Run `docker run -d -p 8080:8080 --name myapp pokemonchallengeapi` 
5. Api is up and running  
   - http://localhost:8080/api/Pokemon/mewtwo
   - http://localhost:8080/api/Translated/mewtwo
   
 ### Run from Local
 1. Run `git clone https://github.com/mkiruba/PokemonChallenge.git`
 2. Open with VS 2022/2019
 3. Run the application
