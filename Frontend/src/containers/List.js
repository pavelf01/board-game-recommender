import { List } from '../components/List'
import { connect } from "react-redux";
import {fetchBoardGames} from "../actions/boardGameListActions";

const mapStateToProps = function (state) {
  return {
    boardGames: state.boardGames.list,
    isFetching: state.boardGames.isFetching,
  }
};

const mapDispatchToProps = function(dispatch) {
  return {
    fetchBoardGames: () => dispatch(fetchBoardGames()),
  }
};

export const ListContainer = connect(mapStateToProps, mapDispatchToProps)(List);
