import * as React from "react";

const listStyle = {
  flexBasis: '70%',
};

export class List extends React.PureComponent {
  constructor(props){
    super(props);
    this.state = {
      searchValue: '',
    }
  }
  componentWillMount(){
    this.props.fetchBoardGames();
  }

  handleSearchInputChange = (event) => {
    this.setState({searchValue: event.target.value});
  };

  search = () => {
    this.props.search(this.state.searchValue)
  }

  render(){
    if(this.props.isFetching){
      return <p>Loading</p>
    }
    return <div style={listStyle} className="card">
      <div className="card-header">
        <h2>Board games</h2>
      </div>
      <div className="input-group">
        <input type="text" className="form-control" placeholder="Search for..." aria-label="Search for..." onChange={this.handleSearchInputChange}></input>
      <span className="input-group-btn">
        <button className="btn btn-secondary" type="button" onClick={this.search}>Go!</button>
      </span>
      </div>
      <ul className="list-group list-group-flush">{this.props.boardGames.map(item => <li onClick={() => this.props.showDetail(item.id)} className="list-group-item" key={item.id}>{item.name}</li>)}</ul>
    </div>
  }
}
