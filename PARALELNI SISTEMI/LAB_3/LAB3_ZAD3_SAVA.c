#include <stdio.h>
#include <mpi.h>

#define n 4
#define m 3

void main(int argc, char** argv)
{

MPI_Comm cart;
int rank, size;
int dims[2] = { n,m };
int periods[2] = {1,0};
int left, right;
int coordsLeft[2], coordsRight[2];

MPI_Init(&argc, &argv);
MPI_Comm_rank(MPI_COMM_WORLD, &rank);
MPI_Comm_size(MPI_COMM_WORLD, &size);

MPI_Cart_create(MPI_COMM_WORLD, 2, dims, periods, 0, &cart);
MPI_Cart_shift(cart, 0, 3, &left, &right);

MPI_Cart_coords(cart, left, 2, coordsLeft);
MPI_Cart_coords(cart, right, 2, coordsRight);

printf("Proces: %d ima levog suseda: %d sa koordinatama [%d][%d] i desnog suseda: %d sa koordinatama [%d][%d]", rank, left,coordsLeft[0],coordsLeft[1],right,coordsRight[0],coordsRight[1]);

MPI_Finalize();
}