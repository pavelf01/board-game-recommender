import * as React from "react";

const detailStyle = {
  flexBasis: '30%',
};

export class Detail extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      rating: 0,
    };

  }

  addRating = () =>{
    this.props.addRating(this.detail.id, this.state.id);
  };

  handleRatingChange = (event) => {
    this.setState({rating: event.target.value});
  };

  render() {
    return (<div style={detailStyle}  className="card">
      <div className="card-body">
        <h4 className="card-title">{this.props.detail.name}</h4>

        <div className="input-group">
          <input type="number" className="form-control" placeholder="Rating" aria-label="Add rating" min={0} max={10} value={this.state.rating} onChange={this.handleRatingChange}></input>
          <span className="input-group-btn">
        <button className="btn btn-secondary" type="button">Add rating</button>
      </span>
        </div>
      </div>
    </div>);
  }
}
