import {FETCH_BOARD_GAME_LIST, FETCH_BOARD_GAME_LIST_SUCCESS, SHOW_DETAIL} from "../actions/boardGameListActions";

const initialState = {
  list: [],
  isFetching: false,
  detail: {
    Id: '',
    Name: '',
  }
};

function boardGamesReducer (state = initialState, action) {
  switch (action.type) {
    case FETCH_BOARD_GAME_LIST:
      return { ...state, isFetching: true};
    case FETCH_BOARD_GAME_LIST_SUCCESS:
      return { ...state, list: action.payload, isFetching: false }
    case SHOW_DETAIL:
      return { ...state, detail: state.list.find(item => item.Id === action.payload)}
  }
  return state;
}

export { boardGamesReducer as boardGames };
