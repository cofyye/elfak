import { useState } from "react";

import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";

const App = () => {
  const [contacts, setContacts] = useState([]);
  const [copyContacts, setCopyContacts] = useState([]);
  const [inputs, setInputs] = useState({
    first_name: "",
    last_name: "",
    phone_number: "",
    email: "",
  });
  const [show, setShow] = useState(false);
  const [op, setOp] = useState("Add");
  const [id, setId] = useState(-1);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const onSearch = (value) => {
    if (!value) {
      setContacts(copyContacts);
      setCopyContacts([]);
    } else {
      const queryString = value.toLowerCase();
      const filteredData = contacts.filter((contact) => {
        const fullName = `${contact.first_name} ${contact.last_name} ${contact.phone_number} ${contact.email}`;

        return fullName.toLowerCase().includes(queryString);
      });

      if (!copyContacts.length) {
        setCopyContacts(contacts);
      }
      setContacts(filteredData);
    }
  };

  const setInputFields = (index) => {
    if (index === -1) {
      alert("Invalid Contact ID");
      return;
    }

    setInputs({
      first_name: contacts[index].first_name,
      last_name: contacts[index].last_name,
      phone_number: contacts[index].phone_number,
      email: contacts[index].email,
    });

    setId(index);
    handleShow();
  };

  const onAddContact = (value) => {
    if (!Object.values(inputs).every((val) => val.length > 0)) {
      alert("Please fill in all fields!");
      return;
    }

    switch (value) {
      case "Add":
        setContacts([...contacts, inputs]);
        break;
      case "Edit":
        Object.assign(contacts[id], inputs);
        break;
      default:
        break;
    }

    setInputs({
      first_name: "",
      last_name: "",
      phone_number: "",
      email: "",
    });

    setId(-1);
  };

  const onDeleteContact = (index) => {
    if (index === -1) {
      alert("Invalid Contact ID");
      return;
    }

    const updatedContacts = contacts.filter((_, i) => i !== index);

    setContacts(updatedContacts);
  };

  return (
    <div className="container">
      <section className="mt-5 d-flex justify-content-between align-items-center">
        <Form.Group className="mb-3" style={{ width: "250px" }}>
          <Form.Control
            type="text"
            placeholder="Search..."
            className="bg-dark border-0 text-white"
            onChange={(event) => onSearch(event.target.value)}
          />
        </Form.Group>
        <div className="mb-3" style={{ width: "250px" }}>
          <Button
            variant="dark"
            onClick={() => [setOp("Add"), handleShow()]}
            className="w-100"
          >
            Add Contact
          </Button>
        </div>
      </section>
      <section className="">
        <div>
          <table className="table table-borderless table-dark table-hover">
            <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">First Name</th>
                <th scope="col">Last Name</th>
                <th scope="col">Phone Number</th>
                <th scope="col">Email</th>
                <th scope="col">Action</th>
              </tr>
            </thead>
            <tbody>
              {contacts.map((contact, index) => {
                return (
                  <tr key={index}>
                    <th scope="row">{index + 1}</th>
                    <td>{contact.first_name}</td>
                    <td>{contact.last_name}</td>
                    <td>{contact.phone_number}</td>
                    <td>{contact.email}</td>
                    <td>
                      <span
                        className="text-primary action-btn mr-2"
                        onClick={() => [setOp("Edit"), setInputFields(index)]}
                      >
                        Edit
                      </span>
                      <span
                        className="text-danger action-btn mx-2"
                        onClick={() => onDeleteContact(index)}
                      >
                        Delete
                      </span>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </section>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header className="bg-dark text-white">
          <Modal.Title>{op} Contact</Modal.Title>
        </Modal.Header>
        <Modal.Body className="bg-dark text-white">
          <Form>
            <Form.Group className="mb-3" controlId="contactForm.FirstName">
              <Form.Label>First Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="First Name"
                className="bg-dark text-white"
                value={inputs.first_name}
                onChange={(event) =>
                  setInputs({ ...inputs, first_name: event.target.value })
                }
                required
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="contactForm.LastName">
              <Form.Label>Last Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Last Name"
                className="bg-dark text-white"
                value={inputs.last_name}
                onChange={(event) =>
                  setInputs({ ...inputs, last_name: event.target.value })
                }
                required
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="contactForm.PhoneNumber">
              <Form.Label>Phone Number</Form.Label>
              <Form.Control
                type="text"
                placeholder="Phone Number"
                className="bg-dark text-white"
                value={inputs.phone_number}
                onChange={(event) =>
                  setInputs({ ...inputs, phone_number: event.target.value })
                }
                required
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="contactForm.Email">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                placeholder="Email"
                className="bg-dark text-white"
                value={inputs.email}
                onChange={(event) =>
                  setInputs({ ...inputs, email: event.target.value })
                }
                required
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer className="bg-dark text-white">
          <Button
            variant="danger"
            onClick={() => {
              setInputs({
                first_name: "",
                last_name: "",
                phone_number: "",
                email: "",
              });
              setId(-1);
              handleClose();
            }}
          >
            Close
          </Button>
          <Button
            variant="dark"
            onClick={() => [onAddContact(op), handleClose()]}
          >
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
      ;
    </div>
  );
};

export default App;
