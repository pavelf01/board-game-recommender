import {FETCH_BOARD_GAME_LIST, FETCH_BOARD_GAME_LIST_SUCCESS} from "../actions/boardGameListActions";

const initialState = {
  list: [],
  isFetching: false,
};

function boardGamesReducer (state = initialState, action) {
  switch (action.type) {
    case FETCH_BOARD_GAME_LIST:
      return { ...state, isFetching: true};
    case FETCH_BOARD_GAME_LIST_SUCCESS:
      return { ...state, list: action.payload, isFetching: false }
  }
  return state;
}

export { boardGamesReducer as boardGames };
