import {connect} from "react-redux";
import {addRating, fetchUserRating, getRecommendations, search} from "../actions/userRatingActions";
import Home from "../components/Home";
import {showDetail} from "../actions/boardGameActions";

const mapStateToProps = function (state) {
  return {
    userRating: state.userRating.userRatings,
    isFetching: state.userRating.isFetching,
    detail: state.userRating.detail,
    recommendations: state.userRating.recommendations,
  }
};

const mapDispatchToProps = function (dispatch) {
  return {
    fetchUserRating: () => dispatch(fetchUserRating()),
    showDetail: (id) => dispatch(showDetail(id)),
    addRating: (id, rating) => dispatch(addRating(id, rating)),
    search: (id) => dispatch(search(id)),
    getRecommendations: (type) => dispatch(getRecommendations(type)),
  }
};

export const HomeContainer = connect(mapStateToProps, mapDispatchToProps)(Home);
