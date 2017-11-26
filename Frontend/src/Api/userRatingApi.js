export function getUserRating() {
  return fetch('http://localhost:62492/api/userRatings/21919')
    .then(function (response) {
      return response.json()
    }).then(function (json) {
      return json;
    }).catch(function (ex) {
      console.log('parsing failed', ex)
    });
}

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

export function getContentBasedRecommendations(id) {
  return fetch(`http://localhost:62492/api/recommendations/content/${id}`)
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}

export function getCollaborativeFilteringRecommendations(id) {
  return fetch(`http://localhost:62492/api/recommendations/collaborative/${id}`)
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}

export function getRandomRecommendations(id) {
  return fetch(`http://localhost:62492/api/recommendations/random/${id}`)
    .then(function(response) {
      return response.json()
    }).then(function(json) {
      return json;
    }).catch(function(ex) {
      console.log('parsing failed', ex)
    });
}
