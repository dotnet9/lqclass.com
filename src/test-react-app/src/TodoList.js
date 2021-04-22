import React, { Component } from 'react';
import 'antd/dist/antd.css';
import store from './store';
import { getInputChangeAction, getAddTODOItemAction, getDeleteTODOItemAction } from './store/actionCreators';
import TodoListUI from './TodoListUI';
import axios from 'axios';

class TodoList extends Component {

    constructor(props) {
        super(props);
        this.state = store.getState();
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleStoreChagne = this.handleStoreChagne.bind(this);
        this.handleBtnClick = this.handleBtnClick.bind(this);
        this.handleItemDelete = this.handleItemDelete.bind(this);
        store.subscribe(this.handleStoreChagne);
    }

    render() {
			return (
				<TodoListUI 
					list={this.state.list}
					inputValue={this.state.inputValue}
					handleInputChange={this.handleInputChange}
					handleBtnClick={this.handleBtnClick}
					handleItemDelete={this.handleItemDelete}
				/>
			)
    }

		componentDidMount() {
			axios.get('/list.json').then(()=>{
				
			});
		}

    handleInputChange(e) {
        const action = getInputChangeAction(e.target.value);
        store.dispatch(action);
    }

    handleStoreChagne() {
        this.setState(store.getState());
    }

    handleBtnClick() {
        const action = getAddTODOItemAction();
        store.dispatch(action);
    }

    handleItemDelete(index) {
        const action = getDeleteTODOItemAction(index);
        store.dispatch(action);
    }
}

export default TodoList;