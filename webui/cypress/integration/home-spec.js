describe('Home', () => {
  beforeEach(() => {
    cy.visit("http://localhost:4200")
  })

  it('show list option', () => {
    cy.contains('List').should('exist');
  })

  it('show search option', () => {
    cy.contains('Search').should('exist')
  })
})
