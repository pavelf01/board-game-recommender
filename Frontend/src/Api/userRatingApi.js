export function getUserRating() {
  //return Promise.resolve([{name: 'Scythe', id: 1}, {name: 'Cyclades', id: 2}]);
  return fetch('http://localhost:62492/api/userRatings/21919')
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}

export function postRating(id, rating) {
  // some post method
}

// returning array of items by substring
export function getById(id) {
  return fetch(`http://localhost:62492/api/userRatings/${id}`)
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}
