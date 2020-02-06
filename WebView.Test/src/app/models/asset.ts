import { Country } from './country';
import { MimeTypeData } from './mime-type';

export class Asset {
    constructor(public AssetId: string, public FileName: string, public CreatedBy: string,
                public Email: string, public Description?: string, public CreatedOn?: string,
                public CountryId?: number, public MimeTypeId?: number, public country?: Country,
                public MimeType?: MimeTypeData) { }
}

export class AssetResult {
    constructor(public Assets: Asset[], public totalPages: number, public currentPage: number) { }
}
