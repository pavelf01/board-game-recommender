import React from "react";
import { Router, Route, IndexRoute } from "react-router";
import { history } from "./store.js";
import App from "./components/App";
import NotFound from "./components/NotFound";
import {HomeContainer} from "./containers/Home";

// build the router
const router = (
  <Router onUpdate={() => window.scrollTo(0, 0)} history={history}>
    <Route path="/" component={App}>
      <IndexRoute component={HomeContainer}/>
      <Route path="*" component={NotFound}/>
    </Route>
  </Router>
);

// export
export { router };
