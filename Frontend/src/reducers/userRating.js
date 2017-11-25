import {FETCH_USER_RATINGS, FETCH_USER_RATINGS_SUCCESS} from "../actions/userRatingActions";
import {SHOW_DETAIL} from "../actions/boardGameActions";

const initialState = {
  list: [],
  isFetching: false,
  detail: {
    Id: '',
    Name: '',
  }
};

function userRatingReducer (state = initialState, action) {
  switch (action.type) {
    case FETCH_USER_RATINGS:
      return { ...state, isFetching: true};
    case FETCH_USER_RATINGS_SUCCESS:
      return { ...state, list: action.payload.map(val => val.BoardGame), isFetching: false }
    case SHOW_DETAIL:
      return { ...state, detail: action.payload }
  }
  return state;
}

export { userRatingReducer as userRating };
