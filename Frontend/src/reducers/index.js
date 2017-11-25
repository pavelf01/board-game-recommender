import { combineReducers } from "redux";
import { routerReducer } from "react-router-redux";
import { reducer as formReducer } from "redux-form";
import { userRating } from "./userRating";

// main reducers
export const reducers = combineReducers({
  routing: routerReducer,
  form: formReducer,
  userRating: userRating,
  // your reducer here
});
