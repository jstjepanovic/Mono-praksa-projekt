import React from 'react'
import { Nav, Navbar, Container } from "react-bootstrap"
import '../css/NavigationBar.css'

function NavigationBar() {
  return (
    <div>
      <Navbar bg="dark" variant="dark" fixed='top' expand="lg" defaultExpanded="true">
        <Container>
          <Nav>
            <Nav.Link href='/'> Home </Nav.Link>
            <Nav.Link href='/marketplace'> Marketplace </Nav.Link>
            <Nav.Link href='/groups'> Groups </Nav.Link>
            <Nav.Link href='/newgame'> New Game </Nav.Link>
            <Nav.Link href='/register'> Register </Nav.Link>
            <Nav.Link href='/users'> Users </Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </div>
  );
}

export default NavigationBar