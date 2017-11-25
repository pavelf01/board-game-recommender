import React from "react";
import { List } from "./List";
import {Detail} from "./Detail";
import {Recommendations} from "./Recommendations";

const containerStyle = {
  display: 'flex',
  flexDirection: 'row',
};

const rightSide = {
  display: 'flex',
  flexDirection: 'column',
  flexBasis: '50%',
  paddingLeft: '15px',
};

export default class Home extends React.PureComponent {
  componentWillMount(){
    this.props.fetchUserRating();
  }

  render() {
    if(this.props.isFetching){
      return <p>Loading</p>
    }

    return (
      <div className="page-home">
        <div style={containerStyle}>
          <List userRating={this.props.userRating} isFetching={this.props.isFetching} showDetail={this.props.showDetail} search={this.props.search} />
          <div style={rightSide}>
            <Detail detail={this.props.detail} addRating={this.props.addRating}/>
            <Recommendations/>
          </div>
        </div>
      </div>
    );
  }
}
