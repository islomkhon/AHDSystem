import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { weatherCreators } from '../store/WeatherForecasts';
import { counterCreators } from '../store/Counter';
import { findDOMNode } from 'react-dom';
import $ from 'jquery';


class Fusion extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        var startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        this.props.requestWeatherForecasts(startDateIndex);
    }

    handleToggle = (e) => {
        const el = findDOMNode(this.refs.toggle);
        $(el).slideToggle();
    };

    render() {
        return (
            <div>
                <h1>Weather forecast</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {renderForecastsTable(this.props)}
                {renderPagination(this.props)}
                <p>Current count: <strong>{this.props.counter.count}</strong></p>
                <button className="btn btn-primary" onClick={this.props.increment}>Increment</button>
                <button className="btn btn-primary" onClick={this.props.decrement}>Decrement</button>

                <div className="ellipsis-click" onClick={this.handleToggle}>
                    <i className="fa-ellipsis-h" />
                    hello
                </div>

            </div>
        );
    }
}

function renderForecastsTable(props) {
    //console.log(props)
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {props.weatherForecasts.forecasts.map(forecast =>
                    <tr key={forecast.dateFormatted}>
                        <td>{forecast.dateFormatted}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

function renderPagination(props) {
    const prevStartDateIndex = (props.weatherForecasts.startDateIndex || 0) - 5;
    const nextStartDateIndex = (props.weatherForecasts.startDateIndex || 0) + 5;

    return <p className='clearfix text-center'>
        <Link className='btn btn-default pull-left' to={`/fusion-data/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/fusion-data/${nextStartDateIndex}`}>Next</Link>
        {props.weatherForecasts.isLoading ? <span>Loading...</span> : []}
    </p>;
}

const mapStateToProps = (state) => {
    return {
        weatherForecasts: state.weatherForecasts,
        counter : state.counter
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        requestWeatherForecasts: (index) => dispatch(weatherCreators.requestWeatherForecasts(index)),
        increment: () => dispatch(counterCreators.increment()),
        decrement: () => dispatch(counterCreators.decrement()),
    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Fusion);


//export default connect(
//    state => state.weatherForecasts,
//    dispatch => bindActionCreators(weatherCreators, dispatch)
//)(Fusion);