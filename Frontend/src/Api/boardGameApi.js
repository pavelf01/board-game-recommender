export function getBoardGameById(id) {
  return fetch(`http://localhost:62492/api/boardGames/${id}`)
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}
