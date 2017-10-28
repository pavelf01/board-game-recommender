import {getBoardGames, getByName, postRating} from "../Api/boardGamesApi";

export const FETCH_BOARD_GAME_LIST = 'FETCH_BOARD_GAME_LIST';
export const FETCH_BOARD_GAME_LIST_SUCCESS = 'FETCH_BOARD_GAME_LIST_SUCCESS';

export const SHOW_DETAIL = 'SHOW_DETAIL';

export function fetchBoardGames() {
  return function (dispatch) {
    dispatch({type: FETCH_BOARD_GAME_LIST});
    getBoardGames().then((boardGames) => {
      dispatch({type: FETCH_BOARD_GAME_LIST_SUCCESS, payload: boardGames})
    });
  }
}

export function showDetail(id) {
  return {type: SHOW_DETAIL, payload: id};
}

export function addRating(id, rating){
  postRating(id, rating);
}

export function search(name) {
  return function (dispatch) {
    dispatch({type: FETCH_BOARD_GAME_LIST});
    getByName().then((boardGames) => {
      dispatch({type: FETCH_BOARD_GAME_LIST_SUCCESS, payload: boardGames})
    });
  }
}
