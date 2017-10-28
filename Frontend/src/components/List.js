import * as React from "react";

export class List extends React.PureComponent {

  componentWillMount(){
    this.props.fetchBoardGames();
  }

  render(){
    if(this.props.isFetching){
      return <p>Loading</p>
    }
    return <div className="card">
      <div className="card-header">
        <h2>Board games</h2>
      </div>
      <ul className="list-group list-group-flush">{this.props.boardGames.map(item => <li className="list-group-item" key={item.id}>{item.name}</li>)}</ul>
    </div>
  }
}
