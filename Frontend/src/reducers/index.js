import { combineReducers } from "redux";
import { routerReducer } from "react-router-redux";
import { reducer as formReducer } from "redux-form";
import {boardGames} from "./boardGames";

// main reducers
export const reducers = combineReducers({
  routing: routerReducer,
  form: formReducer,
  boardGames,
  // your reducer here
});
