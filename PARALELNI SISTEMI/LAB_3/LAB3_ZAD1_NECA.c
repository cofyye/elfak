#include <stdio.h>
#include<stdlib.h>
#include <mpi.h>

#define k 3
#define v 10

void main(int argc, char** argv)
{
int rank, size, i,j, new_comm_rank , new_comm_size , broj = 0 , local_sum = 0 , brojzamat = 0;
int A[k][k] , niz[k];
MPI_Group world , new_group;
MPI_Comm new_world;
MPI_Datatype kolona, kolonaEx;
MPI_Init(&argc, &argv);
MPI_Comm_rank(MPI_COMM_WORLD, &rank);
MPI_Comm_size(MPI_COMM_WORLD, &size);
MPI_Type_vector(k, 1, k, MPI_INT, &kolona);
MPI_Type_commit(&kolona);
MPI_Type_create_resized(kolona, 0, sizeof(int), &kolonaEx);
MPI_Type_commit(&kolonaEx);
if (rank == 0) {
for (i = 0; i < k; i++) {
for (j = 0; j < k; j++) {
A[i][j] = brojzamat++;
}
}
}
int* nonmembers = (int*)malloc(size * sizeof(int));
for (i = 0; i < size; i++)
{
if (i % 5 != 0) {
nonmembers[broj++] = i;
}
}
MPI_Comm_group(MPI_COMM_WORLD, &world);
MPI_Group_excl(world, broj, nonmembers, &new_group);
MPI_Group_rank(new_group, &new_comm_rank);
MPI_Group_size(new_group, &new_comm_size);
if (new_comm_size != k)
return -1;
MPI_Comm_create(MPI_COMM_WORLD, new_group, &new_world);
if (new_world != MPI_COMM_NULL) {
// 0 , 5 , 10
MPI_Scatter(A, 1, kolonaEx, niz, k, MPI_INT, 0, new_world);
for (i = 0; i < k; i++)
local_sum += niz[i];
printf("local sum:%d\n", local_sum);
local_sum > v ? printf("Process : %d ,  ima lokalnu sumu vecu od v", new_comm_rank) : printf("Process : %d ,  nema lokalnu sumu vecu od v", new_comm_rank);
}
free(nonmembers);
MPI_Finalize();
}