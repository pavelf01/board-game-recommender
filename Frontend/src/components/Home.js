import React from "react";
import { List } from "./List";
import {Detail} from "./Detail";

const containerStyle = {
  display: 'flex',
  flexDirection: 'row',
};

export default class Home extends React.PureComponent {
  render() {
    return (
      <div className="page-home">
        <div style={containerStyle}>
          <List fetchBoardGames={this.props.fetchBoardGames} boardGames={this.props.boardGames} isFetching={this.props.isFetching} showDetail={this.props.showDetail} />
          <Detail detail={this.props.detail} addRating={this.props.addRating}/>
        </div>
      </div>
    );
  }
}
