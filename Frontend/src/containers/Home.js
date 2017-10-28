import { connect } from "react-redux";
import {addRating, fetchBoardGames, search, showDetail} from "../actions/boardGameListActions";
import Home from "../components/Home";

const mapStateToProps = function (state) {
  return {
    boardGames: state.boardGames.list,
    isFetching: state.boardGames.isFetching,
    detail: state.boardGames.detail,
  }
};

const mapDispatchToProps = function(dispatch) {
  return {
    fetchBoardGames: () => dispatch(fetchBoardGames()),
    showDetail: (id) => dispatch(showDetail(id)),
    addRating: (id, rating) => dispatch(addRating(id, rating)),
    search: (name) => dispatch(search(name))
  }
};

export const HomeContainer = connect(mapStateToProps, mapDispatchToProps)(Home);
