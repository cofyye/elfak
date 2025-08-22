import { Injectable } from '@nestjs/common';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import { SelectQueryBuilder } from 'typeorm';

@Injectable()
export class PaginationService {
  constructor() {}

  public async find<T>(
    queryBuilder: SelectQueryBuilder<T>,
    queryParams: PaginationQueryDto,
  ): Promise<IPaginationData<T[]>> {
    const itemCount = await queryBuilder.getCount();
    const entities = await queryBuilder.getRawMany();

    return {
      data: entities,
      meta: {
        page: queryParams.page,
        take: queryParams.take,
        itemCount: itemCount,
        pageCount: Math.ceil(itemCount / queryParams.take),
        hasPreviousPage: queryParams.page > 1,
        hasNextPage: queryParams.page < Math.ceil(itemCount / queryParams.take),
      },
    };
  }
}
