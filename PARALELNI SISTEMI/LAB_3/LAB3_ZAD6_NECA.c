#include <stdio.h>
#include<stdlib.h>
#include <mpi.h>

#define n 4
#define m 3

void main(int argc, char** argv)
{
int rank, size;
int dims[2] = { 4,3 };
int periods[2] = { 0 , 1 };
int reorder = 0;
int up, down;
int local_p;
int global_p;
MPI_Comm cart;

MPI_Init(&argc, &argv);
MPI_Comm_rank(MPI_COMM_WORLD, &rank);
MPI_Comm_size(MPI_COMM_WORLD, &size);
MPI_Cart_create(MPI_COMM_WORLD, 2, dims, periods, reorder, &cart);
MPI_Cart_shift(cart, 1, 1, &up, &down);
local_p = up * down;
MPI_Reduce(&local_p, &global_p, 1, MPI_INT, MPI_SUM, 0, cart);
if (rank == 0)
printf("global_p : %d", global_p);
MPI_Finalize();
}