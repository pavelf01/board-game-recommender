import React from "react";
import {getBoardGames} from "../Api/boardGamesApi";
import {ListContainer} from "../containers/List";

const containerStyle = {
  display: 'flex',
  flexDirection: 'row',
};
// Home page component
export default class Home extends React.Component {
  componentWillMount(){
    getBoardGames();
  }
  render() {
    return (
      <div className="page-home">
        <h4>Hello world!</h4>
        <div style={containerStyle}>
          <ListContainer/>
        </div>
      </div>
    );
  }
}
