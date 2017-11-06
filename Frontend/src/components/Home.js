import React from "react";
import { List } from "./List";
import {Detail} from "./Detail";

const containerStyle = {
  display: 'flex',
  flexDirection: 'row',
};

export default class Home extends React.PureComponent {
  componentWillMount(){
    this.props.fetchBoardGames();
  }

  render() {
    if(this.props.isFetching){
      return <p>Loading</p>
    }

    return (
      <div className="page-home">
        <div style={containerStyle}>
          <List boardGames={this.props.boardGames} isFetching={this.props.isFetching} showDetail={this.props.showDetail} search={this.props.search} />
          <Detail detail={this.props.detail} addRating={this.props.addRating}/>
        </div>
      </div>
    );
  }
}
