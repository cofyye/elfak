#include <stdio.h>
#include<stdlib.h>
#include <mpi.h>

#define n 3

void main(int argc, char** argv)
{
int rank, size , new_rank , new_size;
int dims[2] = { n,n };
int periods[2] = { 0 , 0 };
int reorder = 0;
int coords[2];
int local_sum;
MPI_Comm cart , split_comm;

MPI_Init(&argc, &argv);
MPI_Comm_rank(MPI_COMM_WORLD, &rank);
MPI_Comm_size(MPI_COMM_WORLD, &size);
MPI_Cart_create(MPI_COMM_WORLD, 2, dims, periods, reorder, &cart);
MPI_Cart_coords(cart, rank, 2, coords);
MPI_Comm_split(cart, coords[0] >= coords[1], 1, &split_comm);
MPI_Comm_rank(split_comm, &new_rank);
MPI_Comm_size(split_comm, &new_size);
if (coords[0] >= coords[1]) { //u gornjoj
MPI_Reduce(&rank, &local_sum, 1, MPI_INT, MPI_SUM, 0, split_comm);
if (rank == 0) {
printf("local_sum gornja %d\n", local_sum);
MPI_Recv(&local_sum, 1, MPI_INT, 1, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
printf("local_sum donja %d\n", local_sum);
}
}
else { //donjoj
MPI_Reduce(&rank, &local_sum, 1, MPI_INT, MPI_SUM, 0, split_comm);
if (new_rank == 0) {
MPI_Send(&local_sum, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
}
}

MPI_Finalize();
}