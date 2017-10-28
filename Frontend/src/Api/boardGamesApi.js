export function getBoardGames() {
  return Promise.resolve([{name: 'Scythe', id: 1}, {name: 'Cyclades', id: 2}]);
}

export function postRating(id, rating) {
  // some post method
}

// returning array of items by substring
export function getByName(name) {
  return Promise.resolve([{name: 'Scythe', id: 1}]);
}
