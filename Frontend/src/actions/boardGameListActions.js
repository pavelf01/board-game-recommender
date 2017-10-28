import {getBoardGames} from "../Api/boardGamesApi";

export const FETCH_BOARD_GAME_LIST = 'FETCH_BOARD_GAME_LIST';
export const FETCH_BOARD_GAME_LIST_SUCCESS = 'FETCH_BOARD_GAME_LIST_SUCCESS';

export function fetchBoardGames() {
  return function (dispatch) {
    dispatch({type: FETCH_BOARD_GAME_LIST});
    getBoardGames().then((boardGames) => {
      dispatch({type: FETCH_BOARD_GAME_LIST_SUCCESS, payload: boardGames})
    });
  }
};
