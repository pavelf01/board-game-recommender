import {
  getUserRating, getById, getContentBasedRecommendations,
  getCollaborativeFilteringRecommendations, getRandomRecommendations
} from "../Api/userRatingApi";

export const FETCH_USER_RATINGS = 'FETCH_USER_RATINGS';
export const FETCH_USER_RATINGS_SUCCESS = 'FETCH_USER_RATINGS_SUCCESS';

export const FETCH_RECOMMENDATION_SUCCESS = 'FETCH_RECOMMENDATION_SUCCESS';

export function fetchUserRating() {
  return function (dispatch) {
    dispatch({type: FETCH_USER_RATINGS});
    getUserRating().then((boardGames) => {
      dispatch({type: FETCH_USER_RATINGS_SUCCESS, payload: boardGames})
    });
  }
}

export function search(id) {
  return function (dispatch) {
    dispatch({type: FETCH_USER_RATINGS, payload: id});
    getById(id).then((userRating) => {
      dispatch({type: FETCH_USER_RATINGS_SUCCESS, payload: userRating})
    });
  }
}

export function getRecommendations(recommenderType) {
  return function (dispatch, getState) {
    const id = getState().userRating.userId;
    switch(recommenderType) {
      case 'cont':
        getContentBasedRecommendations(id).then((recommendations) => {
          dispatch({type: FETCH_RECOMMENDATION_SUCCESS, payload: recommendations})
        });
        break;
      case 'coll':
        getCollaborativeFilteringRecommendations(id).then((recommendations) => {
          dispatch({type: FETCH_RECOMMENDATION_SUCCESS, payload: recommendations})
        });
        break;
      case 'rand':
        getRandomRecommendations(id).then((recommendations) => {
          dispatch({type: FETCH_RECOMMENDATION_SUCCESS, payload: recommendations})
        });
        break;
      default:
    }

  }
}
