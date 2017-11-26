import {
  FETCH_RECOMMENDATION_SUCCESS, FETCH_USER_RATINGS,
  FETCH_USER_RATINGS_SUCCESS
} from "../actions/userRatingActions";
import {SHOW_DETAIL} from "../actions/boardGameActions";

const initialState = {
  userRatings: [],
  userId: '21919',
  isFetching: true,
  recommendations: [],
  detail: {
    Id: '',
    Name: '',
  }
};

function userRatingReducer (state = initialState, action) {
  switch (action.type) {
    case FETCH_USER_RATINGS:
      return { ...state, isFetching: true, userId: action.payload? action.payload : state.userId};
    case FETCH_USER_RATINGS_SUCCESS:
      return { ...state, userRatings: action.payload, isFetching: false };
    case SHOW_DETAIL:
      return { ...state, detail: action.payload };
    case FETCH_RECOMMENDATION_SUCCESS:
      return { ...state, recommendations: action.payload};
  }
  return state;
}

export { userRatingReducer as userRating };
