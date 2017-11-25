import {getBoardGameById} from "../Api/boardGameApi";
export const SHOW_DETAIL = 'SHOW_DETAIL';

export function showDetail(id) {
  return function (dispatch) {
    getBoardGameById(id).then((boardGame) => {
      dispatch({type: SHOW_DETAIL, payload: boardGame})
    });
  }
}
