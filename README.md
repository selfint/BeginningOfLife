# Beginning Of Life

## Goal

Simulation showing the evolution of different life forms in the ocean, simulating the 'beginning
of life', starting from one simple life form. A submarine will be how one can interact with the
world, with the life forms being able to see and interact with it.

## Algorithm

A genetic algorithm will generate the creature's bodies, as well as their 'logic'. Since the point is
to simulate the *beginning* of life, RNN's with a few neurons and connections should be plenty for
any logic these creatures can evolve. Inspired by [NEAT](http://nn.cs.utexas.edu/downloads/papers/stanley.ec02.pdf), the network will evolve in topology as well as in weight and bias values. Neurons
will be blocks, with each neuron connected to all other neurons the creature has.

## Simplifications

Since this an insane goal, many simplifications will be needed in order to get anything significant evolving in a reasonable amount of time.

### Blocks

This is a powerful simplification, as blocks are easy to simulate at scale, and can abstract many
unnecessary complexities of biological shapes (e.g. minecraft).
